<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="2.0"
xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

  <xsl:output method="html" version="4.0" encoding="iso-8859-1" indent="yes"/>
  <xsl:param name="segmentName"></xsl:param>
  <xsl:param name="position"></xsl:param>
  <!--Pour tests unitaires-->
  <!--<xsl:variable name="segmentName">511</xsl:variable>
  <xsl:variable name="position">46</xsl:variable>-->
  <xsl:template match="/">
    <html>
      <head>
        <title>
          VDA <xsl:value-of select="vda/fileType"/>
        </title>
        <style type="text/css">
          .tableau{border: 1px solid #000000; width: 100%; border-spacing:0; border-collapse:collapse; text-align: right;}
          .tableau td{border: 1px solid #000000; padding: 5px;}
          .tableau th{background-color: #B0C4DE; color:#000000; border: 0px solid #000000; padding: 5px;}
          .tableau tr:hover{background-color: #FF6600;color:#FFFFFF !important;}
          .important{background-color: #FF6600;color:#FFFFFF}
          .important a{color:#FFFFFF}
          .normal{background-color: #FFFFFF; color: #000000}
          .mandatory{color:#8B0000; font-weight:bold; text-align: center}
          .alignleft{text-align: left;}
          .alignCenter{text-align: center;}
        </style>
      </head>
      <body>
        <h1>
          VDA <xsl:value-of select="vda/fileType"/>
        </h1>
        <h2>
          Segment : <xsl:value-of select="//row[rowName=$segmentName][1]/rowName"/>
          <xsl:variable name="MandatoryInfo">
            <xsl:choose>
              <xsl:when test="//row[rowName=$segmentName][1]/mandatory='Y'">
                <xsl:text>Mandatory</xsl:text>
              </xsl:when>
              <xsl:otherwise>
                <xsl:text>Optional</xsl:text>
              </xsl:otherwise>
            </xsl:choose>
          </xsl:variable>
          (<xsl:value-of select="$MandatoryInfo"/>/<xsl:value-of select="//row[rowName=$segmentName][1]/repetition"/>)
        </h2>
        <h3>
          Description : <xsl:value-of select="//row[rowName=$segmentName][1]/description"/>
        </h3>
        <xsl:apply-templates select="//row[rowName=$segmentName]" />
      </body>
    </html>
  </xsl:template>

  <xsl:template match="row">
    <table class="tableau">
      <tr class="alignCenter">
        <th>
          Name
        </th>
        <th>
          Position
        </th>
        <th>
          Length
        </th>
        <th>
          Description
        </th>
      </tr>
      <xsl:apply-templates select="./fields/field"/>
    </table>
  </xsl:template>

  <xsl:template match="field">
    <xsl:choose>
      <xsl:when test="($position >= ./position) and ($position &lt; following::position[../../../rowName=$segmentName][1] or count(following-sibling::field)=0)">
        <tr class="important">
          <td class="alignleft">
            <a href="{./name/text()}:{./position/text()}:{./size/text()}">
              <xsl:value-of select="./name/text()"/>
            </a>
          </td>
          <td>
            <xsl:value-of select="./position/text()"/>
          </td>
          <td>
            <xsl:value-of select="./size/text()"/>
          </td>
          <td class="alignleft">
            <xsl:value-of select="./description/text()"/>
          </td>
        </tr>
      </xsl:when>
      <xsl:otherwise>
        <tr class="cellule">
          <td class="alignleft">
            <a href="{./name/text()}:{./position/text()}:{./size/text()}">
              <xsl:value-of select="./name/text()"/>
            </a>
          </td>
          <td>
            <xsl:value-of select="./position/text()"/>
          </td>
          <td>
            <xsl:value-of select="./size/text()"/>
          </td>
          <td class="alignleft">
            <xsl:value-of select="./description/text()"/>
          </td>
        </tr>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>

</xsl:stylesheet>